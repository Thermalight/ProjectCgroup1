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
    import Dashboard from "./Dashboard.svelte";
    import Navbar from './Navbar.svelte'
    import Sidebar from './Sidebar.svelte'
    import MapMain from './MapMain.svelte';
    import List from './List.svelte';

    let Latitude = -0.09590865581549668;
    let Longitude = 20.90901814937659;
    let mapComponent
    let open = false
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
<!-- <main class="h-screen w-screen">
    <div class="h-1/2 w-screen">
        <MapMain bind:this={mapComponent}/>
    </div>
    <List {mapComponent} class="absolute inset-y-0 right-0 w-16 h-1/2"/>
</main> -->