<script>
    import { slide } from 'svelte/transition';
    import Notification from "./Notification.svelte"
    import Navbar from './Navbar.svelte'
    
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
                refreshRate = 1000;
            })
            .catch((e) => {
                console.log(e);
                refreshRate = 10000;
            })
            .finally
    }
    
</script>
<Navbar/>
<div class="list" transition:slide>
    {#if notifications != null && !loading}
        {#each notifications as event}
            <div transition:slide>
                <Notification event={event}/>
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