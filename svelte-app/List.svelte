<script>
    import { slide } from 'svelte/transition';
    import { onDestroy } from 'svelte';
    import { probability, soundType, notification_count, notifications, refreshRate, unread } from "./stores.js";
    import Notification from "./Notification.svelte"
    import Filter from "./Filter.svelte"
    import isEqual from 'lodash.isequal';

    export let mapComponent

    let clear
    $: {
        clearInterval(clear)
        clear = setInterval(getEvents, $refreshRate)
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
                if (!isEqual($notifications, data)){
                    $notifications = data;
                }
                    
                $refreshRate = 10000;
            })
        })
    }

    onDestroy(() => {
        clearInterval(clear);
    });
</script>
<div class="list" transition:slide>
    {#if $notifications }
        <Filter />
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