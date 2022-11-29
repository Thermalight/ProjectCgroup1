<script>
    import User from './User.svelte';
    import Input from './Input.svelte';
    let searchTerm = "";
    let test = ""
    let UserJson = {username : "", password: "", email: "", IsAdmin: false};
	
	async function submitHandler() {
        console.log(UserJson)
		const response = await fetch("https://localhost/user", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(UserJson),
        });
        location.reload();
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

    // async function deleteUser() {
	// 	const response = await fetch("https://localhost/user", {
    //         method: "DELETE",
    //         headers: {
    //             "Content-Type": "application/json",
    //         },
    //         body: //iets hier,
    //     });
    //     location.reload();
    //     return await response.json();
	// }

    function isAdmin() {
        if (test == "true") {
            UserJson.IsAdmin = true
        }
        else {
           UserJson.IsAdmin = false
        }
        test = ""
        return 
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
        <form on:submit={submitHandler} class="mt-6">
            <Input type="text" label="Username" bind:value={UserJson.username} />
            <Input type="password" label="Password" bind:value={UserJson.password} />
            <Input type="email" label="Email" bind:value={UserJson.email}/>
            <!-- <label class="text-white" for="isadmin">Is Admin: </label> -->
            <Input type="checkbox" label="isAdmin" bind:value={UserJson.IsAdmin}/>
            <!-- <input type="checkbox" id="isadmin" name="isadmin" value={UserJson.IsAdmin = true}<br> -->
            <button class="bg-white px-8 py-2 mt-1 border-none border-r-4 text-gray-800">submit</button>
        </form>
    </div>

    <br><hr>
    <p class="text-white">Users:</p>
    <!-- display users -->
    {#await loadUsers()}
        <p class="text-white">waiting...</p>
    {:then data}
        {#each data as user}
            {#if user.Username.toLowerCase().includes(searchTerm.toLowerCase())}
                <div>
                    <User user={user} /><br>
                </div>
            {/if}
        {/each}
    {:catch error}
        <p>An error occurred!</p>
        {console.log(error)}
    {/await}
</div>
