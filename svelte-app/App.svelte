<script>
    import Dashboard from "./Dashboard.svelte"
    import List from "./List.svelte"
    import Setting from "./Setting.svelte";
    import { currentPage } from "./stores.js";
    import Login from "./login.svelte";
    import Root from "./root.svelte";
    import Navbar from './Navbar.svelte'
    import UserManagement from "./UserManagement.svelte"
    let routes = {
        "notificationpage": List,
        "dashboard": Dashboard,
        "settings":Setting,
        "login":Login,
        "usermanagement": UserManagement,
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

<main class="h-screen w-screen">
    <svelte:component this={routes[$currentPage.toLowerCase()]} />
</main>

