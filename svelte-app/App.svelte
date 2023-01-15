<script>
    import jwt_decode from "jwt-decode";
    import Dashboard from "./Dashboard.svelte"
    import List from "./ListDashboard.svelte"
    import Setting from "./Setting.svelte";
    import { currentPage, notification_count, notifications, probability, refreshRate, soundType, unread } from "./stores.js";
    import Login from "./login.svelte";
    import Root from "./root.svelte";
    import Navbar from './Navbar.svelte'
    import UserManagement from "./UserManagement.svelte"
    import isEqual from 'lodash.isequal';

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

    $: {
        localStorage.getItem("token");
        if (localStorage.getItem("token") && jwt_decode(localStorage.getItem("token")).exp < Date.now() / 1000) {
            localStorage.removeItem("token");
            window.location.pathname = "/login";
        }
    }

    $: {
        $currentPage;
        if ($currentPage == "notificationpage" || $currentPage == "dashboard") {
            $unread = 0;
        }
    }

    function getNew() {
        if (!localStorage.getItem("token")) {
            return;
        }
        fetch("https://" + window.location.host + "/notifications?"+ new URLSearchParams({
            limit: $notification_count,
            soundType: $soundType,
            probability: $probability
        }), {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                "Authorization": 'Bearer ' + localStorage.getItem("token")
            }
        })
        .then(response => {
            response.json()
            .then(data => {
                if (!isEqual($notifications, data)){
                    $notifications = data
                    if ($currentPage == "notificationpage" || $currentPage == "dashboard")
                        $unread = 0;
                    else
                        unread.update(u => u+1);
                }
                    
                $refreshRate = 10000;
            })
        })
    }

    let read
    $: {
        clearInterval(read)
        read = setInterval(getNew, $refreshRate)
    }

</script>
<header>
    {#if $currentPage.toLowerCase() != "login"}
        <Navbar/>
    {/if}
</header>

<main style="z-index: -1;" class="h-screen w-screen">
    <svelte:component this={routes[$currentPage.toLowerCase()]} />
</main>

