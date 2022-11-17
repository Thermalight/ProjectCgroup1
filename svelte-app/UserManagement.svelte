<script>
    import User from './User.svelte';
    import Input from './Input.svelte';
    let searchTerm = "";
    let UserJson = {username : "", password: "", email: "", IsAdmin: true};
	
	async function submitHandler() {
		const response = await fetch("https://localhost/user", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: UserJson.json(),
        });
        return await response.json();
	}

    export async function loadUsers () { 
        const response = await fetch("https://localhost/users", {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
            },
        });
        return await response.json();
    }
</script>
<!-- Searchusers -->
<input bind:value={searchTerm} type="Text" />
{#await loadUsers()}
    <p class="text-white">waiting...</p>
{:then data}
    {#each data as user}
        {#if user.Username.toLowerCase().includes(searchTerm.toLowerCase())}
            <div>
                <User user={user} />
            </div>
        {/if}
    {/each}
{:catch error}
    <p>An error occurred!</p>
    {console.log(error)}
{/await}
<!-- AddUsers -->
<p class="text-white"> -------------------</p>

<form on:submit|preventDefault={submitHandler}>
    <Input label="Username" bind:value={UserJson.username} />
    <Input type="password" label="Password" bind:value={UserJson.password} />
    <Input type="email" label="Email" bind:value={UserJson.email}/>
    <button>click</button>
</form>
<!-- <p class="text-white">{addUsersJson}</p> -->
<p class="text-white"> -------------------</p>
<p class="text-white"> Wat moet er in add users?</p>
<p class="text-white"> Add username</p>
<p class="text-white"> Add e-mail</p>
<p class="text-white"> IsAdmin by default False?</p>
<p class="text-white"> IsSubsrcibed by default False?</p>
<p class="text-white"> Password??</p>

