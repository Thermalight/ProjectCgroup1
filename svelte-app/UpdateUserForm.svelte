<script>
    import Input from './Input.svelte';
    export let user;

    async function updateUser() {
        console.log(user)
		const response = await fetch("https://localhost/user", {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(user), 
        });
        return await response.json();
	}
</script>

<div>
    <aside>
        <form on:submit|preventDefault={updateUser} class="mt-6">
            <input type="hidden" bind:value={user.Guid}/>
            <Input type="text" label="Username" bind:value={user.username} />
            <Input type="password" label="Password" bind:value={user.password} />
            <Input type="email" label="Email" bind:value={user.email}/>
            <label class="text-white" for="isadmin">Is Admin: </label>
            <input type="checkbox" label="isAdmin" bind:checked={user.IsAdmin}/><br>
            <button class="bg-white px-8 py-2 mt-1 border-none border-r-4 text-gray-800">submit</button>
        </form>
    </aside>
</div>