<script>
    import Notification from "./Notification.svelte"
    
    var notifications;
    fetch("https://localhost/notifications", {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        })
        .then(response => response.json())
        .then(data => notifications = data)
        .then(() => console.log(notifications));
</script>
<div class="list">
    {#if notifications != null}
        {#each notifications as event}
            <Notification event={event}/>
        {/each}
    {/if}
</div>
<style>
    .list {
        background-color: #363E4C;
        padding: 10px;
    }
</style>