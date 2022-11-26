<script>
    import Dashboard from "./Dashboard.svelte"
    import List from "./List.svelte"
    import { currentPage } from "./stores.js";
    import Login from "./login.svelte";
    import Root from "./root.svelte";
    import Navbar from './Navbar.svelte'
    let routes = {
        "notificationpage": List,
        "dashboard": Dashboard,
        "login":Login,
        "": Root
    }

    $currentPage = location.pathname.split(/[/?#]/g)[1];
    $: document.title = ($currentPage || "home");
</script>
<style global lang="postcss">

</style>
<header>
    {#if $currentPage.toLowerCase() != "login"}
        <Navbar/>
    {/if}
</header>
<main>
    <svelte:component this={routes[$currentPage.toLowerCase()]} />
</main>