<script>
    import { slide } from 'svelte/transition';
    import { notifications } from "./stores.js";
    import Notification from "./Notification.svelte"
    import Filter from "./Filter.svelte"

    export let mapComponent
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