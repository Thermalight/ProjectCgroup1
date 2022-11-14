<script>
    import User from './User.svelte';
    let users2 = ['Esmee','Gerrit', 'Leon', 'Admin', 'Test123']
    let searchTerm = "";
    //export let listofUser;
</script>

<input bind:value={searchTerm} type="Text">
{#each users2 as user}
    {#if user.toLowerCase().includes(searchTerm.toLowerCase())}
        <User event={user}/>
    {/if}
{/each}







