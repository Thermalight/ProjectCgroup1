<script>
    import { slide } from 'svelte/transition';
    import Notification from "./Notification.svelte"
    export let mapComponent
    
    let notifications;
    let loading = true;
    let refreshRate = 1000;

    let clear
    $: {
        clearInterval(clear)
        clear = setInterval(getEvents, refreshRate)
    }

    function getEvents() {
        fetch("https://localhost/notifications", {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(response => response.json())
            .then(data => notifications = data)
            .then(() => {
                loading = false;
                refreshRate = 60000;
            })
    }
    
</script>
<!-- <Navbar/> -->
<div class="list" transition:slide>
    {#if notifications != null && !loading}
        {#each notifications as event}
            <div transition:slide>
                <Notification {mapComponent} event={event}/>
            </div>
        {/each}
    {:else}
        <div>Loading...</div>
    {/if}
</div>
<style>
    .list {
        background-color: #363E4C;
        padding: 10px;
    }
</style>