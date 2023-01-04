<script>
    import User from './User.svelte';
    import Input from './Input.svelte';
    import UpdateUserForm from './UpdateUserForm.svelte';
    let searchTerm = "";
    let updateUserFormOpen = false
    let addUserFormOpen = false
    let formsClosed = true;
    let UserJson = {username : "", password: "", email: "", IsAdmin: false};
    let updateUserJson = {username : "", password: "", email: "", IsAdmin: false};
	
	async function submitHandler() {
        if (UserJson.username && UserJson.password && UserJson.email) {
            const response = await fetch("https://" + window.location.host + "/user", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(UserJson),
        });
        return await response.json();
        }
        return;
	}

    export async function loadUsers() { 
        const response = await fetch("https://" + window.location.host + "/users", {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
            },
        });
        return await response.json();
    }

    async function deleteUser(guid) {
		const response = await fetch("https://" + window.location.host + "/user/" + guid, {
            method: "DELETE",
        });
        return await response.json();
	}

    function updateFormHandler(user) {
        updateUserJson = {username: user.Username, email: user.Email, IsAdmin: user.IsAdmin, Guid: user.Guid};
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

<div style="background-color:#363e4c; height: 90%;" class="w-5/6 m-auto rounded-2xl">
    <div style="background-color:#111727;"class="flex justify-center h-16 rounded-t-2xl p-auto">
        <h1 class="text-2xl text-white m-auto">User management</h1>
    </div>
   
    <div>
        <!-- input form to add users -->
        <button style="background-color:#111727" class="text-white font-medium rounded-lg text-sm sm:w-auto px-5 py-2.5 text-center border-none ml-6 mt-6 p-auto" type="submit"on:click={() => addUserForm()}>add user</button>
        {#if updateUserFormOpen}
            <button style="background-color:#111727" class="text-white font-medium rounded-lg text-sm sm:w-auto px-5 py-2.5 text-center border-none mr-6 mt-6 p-auto" type="submit"on:click={() => cancel()}>cancel changes</button>
        {/if}
        {#if addUserFormOpen}
            <div style="background-color:#111727;" class="m-6 grid rounded-2xl content-center">
                <p class="block mb-2 text-sm font-medium dark:text-white text-white text-center">Add User</p>
                <form on:submit|preventDefault={submitHandler} class="pl-4 my-6 mr-6">
                    <Input type="text" label="Username" bind:value={UserJson.username} />
                    <Input type="password" label="Password" bind:value={UserJson.password} />
                    <Input type="email" label="Email" bind:value={UserJson.email}/>
                    <label class="text-white" for="isadmin">Is Admin: </label>
                    <input type="checkbox" label="isAdmin" bind:checked={UserJson.IsAdmin}/><br>
                    <button class="bg-white px-8 py-2 mt-1 border-none border-r-4 text-gray-800">submit</button>
                </form>
            </div>
        {/if}
        <div>
            <!-- input field to filter users -->
            <div style="background-color:#111727;" class="mx-6 mt-6 grid rounded-2xl content-center">
                <p class="block mb-2 font-medium dark:text-white text-white text-center">Filter by name </p>
                <label class="p-auto m-auto content-center" for="">
                    <p class="text-white text-center text-sm">Type in username:</p>
                    <input class="mb-2" bind:value={searchTerm} type="text" />
                </label>
            </div>
        </div>
    </div>

    {#if updateUserFormOpen}
        <div style="background-color:#111727;" class="m-6 grid rounded-2xl content-center">
            <p class="block mb-2 text-sm font-medium dark:text-white text-white text-center">Update user </p>
            <UpdateUserForm user={updateUserJson}/>
        </div>
    {/if}

    {#if formsClosed}
        <div style="background-color:#363e4c; height: 55%;" class="m-6 mb-0 content-center overflow-auto">
            <!-- display users -->
            {#await loadUsers()}
                <p class="text-white">waiting...</p>
            {:then data}
            <div class="user overflow-auto">
                {#each data as user}
                    {#if user.Username.toLowerCase().includes(searchTerm.toLowerCase())}
                        <div class="userEntry p-4 text-white bg-primary-dark mb-2 mt-2 rounded-lg">
                            <User user={user} />
                            <button class="text-white border-transparent focus:border-transparent focus:ring-0" type="submit" on:click={() => deleteUser(user.Guid)}><span class="material-symbols-outlined">delete</span></button>
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
    #users{
        width: inherit;
        height: inherit;
    }
</style>