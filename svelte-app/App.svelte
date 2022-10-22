<script>
    import Dashboard from "./Dashboard.svelte"
    import List from "./List.svelte"
    import { currentPage } from "./stores.js";

    let routes = {
        "notificationpage": List,
        "": Dashboard
    }

    $currentPage = location.pathname.split(/[/?#]/g)[1];
    $: document.title = ($currentPage || "home");
</script>
<style global lang="postcss">

</style>
<main>
    <svelte:component this={routes[$currentPage.toLowerCase()]} />
</main>