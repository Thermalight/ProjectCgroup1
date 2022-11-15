<script>
    import User from './User.svelte';

    let searchTerm = "";

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