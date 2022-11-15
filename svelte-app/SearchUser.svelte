<script>
    import User from './User.svelte';
    import { loadUsers } from './user-management.svelte';
    let searchTerm = "";
    // const test = (async () => {
    //     const response = await fetch("https://localhost/users", {
    //         method: "GET",
    //         headers: {
    //             "Content-Type": "application/json",
    //         },
    //     });
    //     return await response.json();
    // })();
</script>

<input bind:value={searchTerm} type="Text" />
{#await loadUsers()}
    <p>...waiting</p>
{:then data}
    {#each data as user}
        {#if user.Username.toLowerCase().includes(searchTerm.toLowerCase())}
            <div>
                <User event={user} />
            </div>
        {/if}
    {/each}
{:catch error}
    <p>An error occurred!</p>
{/await}

