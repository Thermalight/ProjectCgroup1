<script>
    import Input from './Input.svelte';
    export let user;
    let pass = "";
    let passConfirm = "";

    async function submitHandler() {
        if (pass == "" || (pass == passConfirm && pass && passConfirm)) {
            user.password = pass;
        } 
        else {
            return;
        }
		const response = await fetch("https://" + window.location.host + "/users", {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
                "Authorization": 'Bearer ' + localStorage.getItem("token")
            },
            body: JSON.stringify(user), 
        });
        return await response;
	}
   
</script>

<div>
    <aside>
        <form on:submit|preventDefault={submitHandler} class="pl-4 my-6 mr-6">
            <input type="hidden" bind:value={user.guid}/>
            <Input type="text" label="Username" bind:value={user.username} />
            <Input type="password" label="Password" required bind:value={pass} />
            {#if pass}
                <Input type="password" label="Confirm Password" bind:value={passConfirm} />
            {/if}
            <Input type="email" label="Email" bind:value={user.email}/>
            <label class="text-white" for="isadmin">Is Admin: </label>
            <input type="checkbox" label="isAdmin" bind:checked={user.isAdmin}/><br>
            <button class="bg-white px-8 py-2 mt-1 border-none border-r-4 text-gray-800">submit</button>
        </form>
    </aside>
</div>