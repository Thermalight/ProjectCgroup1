<script>
    import User from './User.svelte';
    import Input from './Input.svelte';
    import UpdateUserForm from './UpdateUserForm.svelte';
    let searchTerm = "";
    let updateUserFormOpen = false
    let addUserFormOpen = false
    let formsClosed = true;
    let UserJson = {
        username : "", 
        password: "", 
        email: "", 
        isAdmin: false
    };
    let data;
    let updateUserJson = {username : "", password: "", email: "", isAdmin: false};

    async function reload(){
        data = await loadUsers();
    }
	
	async function submitHandler() {
        if (UserJson.username && UserJson.password && UserJson.email) {
                const response = await fetch("https://" + window.location.host + "/users", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": 'Bearer ' + localStorage.getItem("token")
                },
                body: JSON.stringify(UserJson),
            });
            reload();
            UserJson = {username : "", password: "", email: "", isAdmin: false};
            return await response.json();
        }
        return;
	}

    export async function loadUsers() { 
        const response = await fetch("https://" + window.location.host + "/users", {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Authorization": 'Bearer ' + localStorage.getItem("token")
            },
        });
        return await response.json();
    }

    async function deleteUser(guid) {
		const response = await fetch("https://" + window.location.host + "/users/" + guid, {
            headers: {
                "Content-Type": "application/json",
                "Authorization": 'Bearer ' + localStorage.getItem("token")
            },
            method: "DELETE",
        });
        reload();
        return await response.json();
	}

    function updateFormHandler(user) {
        updateUserJson = {username: user.username, email: user.email, isAdmin: user.isAdmin, guid: user.guid};
        updateUserFormOpen = !updateUserFormOpen;
        formsClosed = !formsClosed;
    }

    function cancel() {
        updateUserFormOpen = false;
        formsClosed = !formsClosed;
    }

    function addUserForm() {
        addUserFormOpen = !addUserFormOpen;
        formsClosed = !formsClosed;
    }
</script>

<div style="background-color:#363e4c; height: 100%;" class="m-auto">
    <div style="background-color:#111727;"class="flex justify-center h-16 p-auto">
        <h1 class="text-2xl text-white m-auto">User management</h1>
    </div>
   
    <div>
        <!-- input form to add users -->
        {#if updateUserFormOpen}
            <button style="background-color:#111727" class="text-white font-medium rounded-lg text-sm sm:w-auto px-5 py-2.5 text-center border-none ml-6 mt-6 p-auto" type="submit"on:click={() => cancel()}>Back</button>
        { :else }
            <button style="background-color:#111727" class="text-white font-medium rounded-lg text-sm sm:w-auto px-5 py-2.5 text-center border-none ml-6 mt-6 p-auto" type="submit"on:click={() => addUserForm()}>{addUserFormOpen ? "Back " : "Add user"}</button>
        {/if}
        {#if addUserFormOpen }
            <p class="mb-2 text-xl font-medium dark:text-white text-white text-center m-auto w-full">Create account</p>
            <div class="p-4">
                <div style="background-color:#111727;" class="grid rounded-2xl content-center relative w-full">
                    <form on:submit|preventDefault={submitHandler} class="px-12 pt-12 pb-16 w-full relative">
                        <Input type="text" icon="person" label="Username" bind:value={UserJson.username} />
                        <Input type="password" icon="lock" label="Password" bind:value={UserJson.password} />
                        <Input type="email" icon="mail" label="Email" bind:value={UserJson.email}/>
                        <p class="text-white">Account Type:</p>
                        <div class="pr-1 relative">
                            <span class="material-symbols-outlined text-white relative top-1 -left-1">
                                shield
                            </span>
                            <select bind:value={UserJson.isAdmin} class="mt-2 mb-5 p-2 px-3 text-white bg-gray-800 border-0 rounded-full">
                                <option value={false}>User</option>
                                <option value={true}>Admin</option>
                            </select>
                        </div>
                        <br>
                        <button style="transform: translate(-50%, 0);" class="absolute left-1/2 bg-gray-800 px-8 py-2 border-0 text-white rounded-md">submit</button>
                    </form>
                </div>
            </div>
        { :else if !updateUserFormOpen }
            <div>
                <!-- input field to filter users -->
                <label class="flex mx-6 mt-6 rounded-2xl content-center relative bg-white items-center">
                    <span class="material-symbols-outlined ml-2">
                        search
                    </span>
                    <input class="flex-grow-[1] p-2 w-full bg-transparent border-none focus:outline-none" bind:value={searchTerm} type="text" placeholder="Username"/>
                </label>
            </div>
        { /if }
    </div>

    {#if updateUserFormOpen}
        <p class="mb-2 text-xl font-medium dark:text-white text-white text-center m-auto w-full">Update user </p>
        <div style="background-color:#111727;" class="m-6 grid rounded-2xl content-center">
            <UpdateUserForm user={updateUserJson}/>
        </div>
    {/if}

    {#if formsClosed}
        <div style="background-color:#363e4c; height: 100%;" class="m-6 mb-0 content-center overflow-auto">
            <!-- display users -->
            {#await reload()}
                <p class="text-white">waiting...</p>
            {:then}
            <div class="user overflow-auto pb-72">
                {#each data as user}
                    {#if user.username.toLowerCase().includes(searchTerm.toLowerCase())}
                        <div class="userEntry p-4 text-white bg-primary-dark mb-2 mt-2 rounded-lg">
                            <User user={user} />
                            <button class="text-white border-transparent focus:border-transparent focus:ring-0" type="submit" on:click={() => deleteUser(user.guid)}><span class="material-symbols-outlined">delete</span></button>
                            <button class="text-white border-transparent focus:border-transparent focus:ring-0 pl-5" type="submit"on:click={() => updateFormHandler(user)}><span class="material-symbols-outlined">manage_accounts</span></button> 
                        </div>
                    {/if}
                {/each}
            </div>
            {:catch error}
                <p>An error occurred!</p>
                {console.log(error)}
            {/await}
        </div>
    {/if}
</div>
<style>
    form > * {
        margin: auto;
        text-align: center;
    }
</style>