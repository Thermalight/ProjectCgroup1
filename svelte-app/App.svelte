<script>
    import jwt_decode from "jwt-decode";
    import Dashboard from "./Dashboard.svelte"
    import List from "./ListDashboard.svelte"
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

    if (!location.pathname.startsWith("/login") && !localStorage.getItem("token")) {
        window.location.pathname = "/login";
    }

    if (localStorage.getItem("token") && jwt_decode(localStorage.getItem("token")).exp < Date.now() / 1000) {
        localStorage.removeItem("token");
        window.location.pathname = "/login";
    }

    // send the user to the dashboard if the user is not an admin
    if (location.pathname.startsWith("/usermanagement") && jwt_decode(localStorage.getItem("token")).admin != "True") {
        window.location.pathname = "/dashboard";
        $currentPage = "dashboard";
    }

</script>
<header>
    {#if $currentPage.toLowerCase() != "login"}
        <Navbar/>
    {/if}
</header>

<main class="h-screen w-screen">
    <svelte:component this={routes[$currentPage.toLowerCase()]} />
</main>

