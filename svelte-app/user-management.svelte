
 <script>
    import User from './User.svelte';
    import SearchUser from './SearchUser.svelte';;
 

 
    var Users;
    fetch("https://localhost/users", {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => Users = data)
        .finally
</script>
<div class="listOfUsers">
 {#if Users != null}
     {#each Users as event}
         <div>
             <User event={event}/>
         </div>
     {/each}
 {:else}
     <div>No users available</div>
 {/if}
</div>


<SearchUser/>

