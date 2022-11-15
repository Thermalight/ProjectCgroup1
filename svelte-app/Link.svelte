<script>
    import goToPage from "./goToPage.js";
    import { createEventDispatcher } from "svelte";

    export let href;

    const dispatch = createEventDispatcher();

    function handleClick(e) {
        e.preventDefault();
        dispatch("click");
        goToPage(href);
    }
</script>

<a {href} on:click={handleClick} title={href} draggable="false">
    <slot />
</a>