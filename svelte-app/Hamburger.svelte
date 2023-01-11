<script>
    export let open;
</script>

<button class="text-gray-500 hover:text-white cursor-pointer border-none focus:outline-none" class:open on:click={() => open = !open}>
    <span class="material-symbols-outlined text-3xl"> menu </span>
</button>

<style>
    button {
        z-index: 20;
    }
</style>