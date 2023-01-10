<script>
    import { slide } from 'svelte/transition';
    import { onDestroy, onMount } from 'svelte';
    import { probability, soundType, notification_count, notifications, changedNotifications } from "./stores.js";
    import Notification from "./Notification.svelte"
    import Filter from "./Filter.svelte"
    export let mapComponent

    let loading = true;
    let refreshRate = 1000;
    let changed = false;
    let filterBool = false

    let handleSubmit = () => {
        $changedNotifications = true
        changed = true;
    }

    let clear
    $: {
        clearInterval(clear)
        clear = setInterval(getEvents, refreshRate)
    }

    function getEvents() {
        fetch("https://" + window.location.host + "/notifications?"+ new URLSearchParams({
            limit: $notification_count,
            soundType: $soundType,
            probability: $probability
        }), {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                "Authorization": 'Bearer ' + localStorage.getItem("token")
            }
        })
        .then(response => {
            response.json()
            .then(data => {
                $notifications = data
                changed = true
                $changedNotifications = true
            })
        })
        .then(() => {
            loading = false;
            changed = true
            refreshRate = 10000;
        })
    }

    onDestroy(() => {
        clearInterval(clear);
    });
</script>
<div class="list" transition:slide>
    {#if $notifications && !loading}
        <Filter bind:changed={changed} bind:filterBool={filterBool} bind:handleSubmit={handleSubmit} />
        {#if changed && !filterBool}
            {handleSubmit() ? "" : ""}
        {/if}
        {#each $notifications as event}
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