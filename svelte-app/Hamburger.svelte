<script>
    export let open;
    import { unread } from './stores.js'
</script>

<button class="text-gray-500 hover:text-white cursor-pointer border-none focus:outline-none relative" class:open on:click={() => open = !open}>
    <span class="material-symbols-outlined text-3xl"> menu </span>
    { #if $unread > 0 }
        <p class="absolute -top-1 -right-3 h-9/12 w-9/12 text-white bg-red-500 border-1 rounded-full">
            { $unread }
        </p>
    { /if }
</button>

<style>
    button {
        z-index: 20;
    }
</style>