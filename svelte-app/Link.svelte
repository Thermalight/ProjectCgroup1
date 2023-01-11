<script>
    import goToPage from "./goToPage.js";
    import { createEventDispatcher } from "svelte";

    export let href;
    export let unread;

    const dispatch = createEventDispatcher();

    function handleClick(e) {
        e.preventDefault();
        dispatch("click");
        goToPage(href);
    }
</script>

<a class="text-white" {href} on:click={handleClick} title={href} draggable="false">
    <slot />
    { #if unread }
        { #if unread > 0 }
            <span class="px-2 text-white bg-red-500 border-1 rounded-full">
                { unread }
            </span>
        { /if }
    { /if }
</a>