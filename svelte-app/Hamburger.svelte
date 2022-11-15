<script>
    export let open = false
</script>

<button class="text-gray-500 hover:text-gray-700 cursor-pointer mr-4 border-none focus:outline-none" class:open on:click={() => open = !open}>
    <span class="material-symbols-outlined"> menu </span>
</button>

<style>
    button {
        z-index: 20;
    }
</style>