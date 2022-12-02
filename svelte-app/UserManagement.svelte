<script>
    import User from './User.svelte';
    import Input from './Input.svelte';
    import UpdateUserForm from './UpdateUserForm.svelte';
    let searchTerm = "";
    let updateUserFormOpen = false
    let UserJson = {username : "", password: "", email: "", IsAdmin: false};
    let updateUserJson = {username : "", password: "", email: "", IsAdmin: false};
	
	async function submitHandler() {
        console.log(UserJson)
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
        console.log(guid)
        return await response.json();
	}

    function updateFormHandler(user) {
        console.log(user)
        updateUserJson = {username: user.Username, email: user.Email, IsAdmin: user.IsAdmin, Guid: user.Guid};
        updateUserFormOpen = !updateUserFormOpen;
    }
</script>

<div >
    <div>
        <!-- input field to filter users -->
        <p class="text-white">filter by name:</p>
        <input bind:value={searchTerm} type="Text" />
    </div>
    <div>
        <!-- input form to add users -->
        <form on:submit|preventDefault={submitHandler} class="mt-6">
            <Input type="text" label="Username" bind:value={UserJson.username} />
            <Input type="password" label="Password" bind:value={UserJson.password} />
            <Input type="email" label="Email" bind:value={UserJson.email}/>
            <label class="text-white" for="isadmin">Is Admin: </label>
            <input type="checkbox" label="isAdmin" bind:checked={UserJson.IsAdmin}/><br>
            <button class="bg-white px-8 py-2 mt-1 border-none border-r-4 text-gray-800">submit</button>
        </form>
    </div>

    {#if updateUserFormOpen}
        <UpdateUserForm user={updateUserJson}/>
    {/if}

    <br><hr>
    <p class="text-white">Users:</p>
    <!-- display users -->
    {#await loadUsers()}
        <p class="text-white">waiting...</p>
    {:then data}
        <div class="users">
            {#each data as user}
                {#if user.Username.toLowerCase().includes(searchTerm.toLowerCase())}
                    <div>
                        <User user={user} /><br>
                        <button class="text-white" type="submit" on:click={() => deleteUser(user.Guid)}>x</button>
                        <button class="text-white" type="submit"on:click={() => updateFormHandler(user)}>update</button>
                    </div>
                {/if}
            {/each}
        </div>
    {:catch error}
        <p>An error occurred!</p>
        {console.log(error)}
    {/await}
</div>
