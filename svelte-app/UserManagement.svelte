<script>
    import User from './User.svelte';
    import Input from './Input.svelte';
    import UpdateUserForm from './UpdateUserForm.svelte';
    let searchTerm = "";
    let updateUserFormOpen = false
    let UserJson = {username : "", password: "", email: "", IsAdmin: false};
    let updateUserJson = {username : "", password: "", email: "", IsAdmin: false};
	
	async function submitHandler() {
		const response = await fetch("https://localhost/user", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(UserJson),
        });
        return await response.json();
	}

    export async function loadUsers() { 
        const response = await fetch("https://localhost/users", {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
            },
        });
        return await response.json();
    }

    async function deleteUser(guid) {
		const response = await fetch("https://localhost/user/" + guid, {
            method: "DELETE",
        });
        return await response.json();
	}

    function updateFormHandler(user) {
        updateUserJson = {username: user.Username, email: user.Email, IsAdmin: user.IsAdmin, Guid: user.Guid};
        updateUserFormOpen = !updateUserFormOpen;
    }
</script>
<div style="background-color:#363e4c; height: 90%;" class="w-5/6 m-auto rounded-2xl">
    <div style="background-color:#111727;"class="flex justify-center   h-16 rounded-t-2xl p-auto">
        <h1 class="text-2x1 text-white m -auto">Users management</h1>
    </div>
    <div>
        <!-- input field to filter users -->
        <div style="background-color:#111727;" class="m-6 grid rounded-2xl content-center">
            <p class="block mb-2 text-sm font-medium dark:text-white text-white text-center">Filter by name </p>
            <p class="text-white inline-block">Type in username:</p>
            <input bind:value={searchTerm} type="Text" />
        </div>
    </div>
    <div>
        <!-- input form to add users -->
        <div style="background-color:#111727;" class="m-6 grid rounded-2xl content-center">
            <p class="block mb-2 text-sm font-medium dark:text-white text-white text-center">Add User</p>
            <form on:submit|preventDefault={submitHandler} class="mt-6">
                <Input type="text" label="Username" bind:value={UserJson.username} />
                <Input type="password" label="Password" bind:value={UserJson.password} />
                <Input type="email" label="Email" bind:value={UserJson.email}/>
                <label class="text-white" for="isadmin">Is Admin: </label>
                <input type="checkbox" label="isAdmin" bind:checked={UserJson.IsAdmin}/><br>
                <button class="bg-white px-8 py-2 mt-1 border-none border-r-4 text-gray-800">submit</button>
            </form>
        </div>
    </div>

    {#if updateUserFormOpen}
            <div style="background-color:#111727;" class="m-6 grid rounded-2xl content-center">
                <p class="block mb-2 text-sm font-medium dark:text-white text-white text-center">Update user </p>
                <UpdateUserForm user={updateUserJson}/>
            </div>
    {/if}

    <!-- <br><hr> -->
    <div style="background-color:#111727; height: 30%;" class="m-6 mb-0 content-center overflow-auto">
        <p class="block mb-2 text-sm font-medium dark:text-white text-white text-center">Users:</p>
        <!-- display users -->
        {#await loadUsers()}
            <p class="text-white">waiting...</p>
        {:then data}
        <div class="user  overflow-auto">
            {#each data as user}
                {#if user.Username.toLowerCase().includes(searchTerm.toLowerCase())}
                    <div>
                        <User user={user} />
                        <button class="text-white" type="submit" on:click={() => deleteUser(user.Guid)}>x</button>
                        <button class="text-white" type="submit"on:click={() => updateFormHandler(user)}>update</button>
                        
                    </div><br>
                {/if}
            {/each}
        </div>
        {:catch error}
            <p>An error occurred!</p>
        {/await}
    </div>
</div>
<style>
    #users{
        width: inherit;
        height: inherit;
    }
</style>