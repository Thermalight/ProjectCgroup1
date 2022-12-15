<script>
    import { slide } from 'svelte/transition';
    import Notification from "./Notification.svelte"
    import Filter from "./Filter.svelte"
    export let mapComponent
    
    let notifications;
    let loading = true;
    let refreshRate = 1000;
    export let addNotifs
    let changed
    let filterBool = false

    let handleSubmit = () =>{
        addNotifs(notifications, true)
        changed = !changed
    }

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
            .then(data => {
                notifications = data
            })
            .then(() => {
                loading = false;
                changed = true
                refreshRate = 10000;
            })
    }
</script>
<div class="list" transition:slide>
    {#if notifications != null && !loading}
        <Filter bind:notifications={notifications} bind:changed={changed} bind:filterBool={filterBool} bind:handleSubmit={handleSubmit} />
        {#if changed && !filterBool}
            {handleSubmit()}
        {/if}
        {#each notifications as event}
            <div transition:slide>
                <Notification {mapComponent} event={event} />
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