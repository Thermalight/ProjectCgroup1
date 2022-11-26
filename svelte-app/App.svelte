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
    export let name;
    import DashboardData from "./Dashboard.svelte";
</script>
<style global lang="postcss">

</style>
<header>
    {#if $currentPage.toLowerCase() != "login"}
        <Navbar/>
    {/if}
</header>

<main class="h-screen w-screen">
    <svelte:component this={routes[$currentPage.toLowerCase()]} />
</main>

<!-- <main class="h-screen w-screen">
    <DashboardData/>
</main> -->
