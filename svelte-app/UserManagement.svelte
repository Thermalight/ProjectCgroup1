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
<div class="h-full">
    <div style="background-color:#363e4c;"class="block  h-4/6 w-5/6 m-auto rounded-2xl">
        <div style="background-color:#111727;"class="flex justify-center   h-16 rounded-t-2xl p-auto">
        </div>
        <div style="height: 89%;"class="grid w-4/5 content-center m-auto py-16">
            <form method="POST" action= "/usermanagement" name="usermanagement">
                <div style="background-color:#111727;" class="m-6 grid rounded-2xl content-center">
                    <label for="Add users" class="block mb-2 text-sm font-medium dark:text-white text-white text-center">Add users <p id="addUsers" class="text-white inline-block">test</p></label>
                    <p id="addUsername" class="text-white inline-block">Add username</p><input  id="Add username" type="text" class="block mb-2 text-sm font-medium dark:text-white text-black text-center">
                    <p id="addPassword" class="text-white inline-block">Add password</p><input  id="Add password" type="text" class="block mb-2 text-sm font-medium dark:text-white text-black text-center">
                    <p id="addEmail" class="text-white inline-block">Add e-mail</p><input  id="Add e-mail" type="text" class="block mb-2 text-sm font-medium dark:text-white text-black text-center">
                    <p id="addIsAdmin" class="text-white inline-block">Is admin [Add checkmarkbox thingy]</p>
                </div>
                <div style="background-color:#111727;" class="m-6 grid rounded-2xl content-center">
                    <label for="Delete users" class="block mb-2 text-sm font-medium dark:text-white text-white text-center">Delete users <p id="deleteUsers" class="text-white inline-block">test</p></label>
                    <p id="Show users" class="text-white inline-block">There should be users here idk how</p>
                </div>
                <div style="background-color:#111727;" class="m-6 grid rounded-2xl content-center">
                    <label for="Update users" class="block mb-2 text-sm font-medium dark:text-white text-white text-center">Update users <p id="updateUsers" class="text-white inline-block">test</p></label>
                    <p id="Show users" class="text-white inline-block">List of users you want to change, idk how</p>
                    <p id="changeUsername" class="text-white inline-block">Change username</p><input  id="Change username" type="text" class="block mb-2 text-sm font-medium dark:text-white text-black text-center">
                    <p id="changePassword" class="text-white inline-block">Change password</p><input  id="Change password" type="text" class="block mb-2 text-sm font-medium dark:text-white text-black text-center">
                    <p id="changeEmail" class="text-white inline-block">Change e-mail</p><input  id="Change e-mail" type="text" class="block mb-2 text-sm font-medium dark:text-white text-black text-center">
                    <p id="changeIsAdmin" class="text-white inline-block">Change is admin [Add checkmarkbox thingy]</p>

                </div>

            </form>
        </div>
    </div>
</div>
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