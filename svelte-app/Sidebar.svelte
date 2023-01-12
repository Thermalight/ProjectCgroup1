<script>
    import Link from "./Link.svelte"
    import jwt_decode from "jwt-decode";
    export let open;
    import { unread } from './stores.js'

    const close = () => open = false;
    const logout = () => localStorage.removeItem("token");
</script>

<div>
    { #if open }
    <div style="z-index: 50;" class="w-screen h-screen absolute bg-black opacity-60 top-0 left-0" on:click={close} />
    { /if }
    <aside style="background-color:#111727; z-index: 100; box-shadow: -20px 0 0 0 #111727;" class="absolute max-w-max h-full top-0 bottom-0" class:open>
        <nav class="p-12 pt-8 pl-6 text-xl font-mono">
            <span class="text-white material-symbols-outlined absolute -right-3 top-6 bg-primary-light p-3 rounded-full shadow-2xl" on:click={close}>
                arrow_back
            </span>
            <ul>
                <li class="text-white font-bold pb-24">
                    <p>
                        Chengeta Wildlife 
                    </p>
                </li>
                <li class="pb-4">
                    <span class="material-symbols-outlined relative top-1 mr-1">
                        home
                    </span>
                    <Link on:click={close} unread={$unread} href="/">
                        Dashboard
                    </Link>
                </li>
                <li class="pb-4">
                    <span class="material-symbols-outlined relative top-1 mr-1">
                        list
                    </span>
                    <Link on:click={close} href="notificationpage">
                        Notifications
                    </Link>
                </li>
                {#if jwt_decode(localStorage.getItem("token")).admin == "True"}
                    <li class="pb-4">
                        <span class="material-symbols-outlined relative top-1 mr-1">
                            admin_panel_settings
                        </span>
                        <Link on:click={close} href="usermanagement">
                            User Management
                        </Link>
                    </li>
                {/if}
                <li class="pb-4">
                    <span class="material-symbols-outlined relative top-1 mr-1">
                        settings
                    </span>
                    <Link on:click={close} href="settings">
                        Settings
                    </Link>
                </li>
                <li class="absolute bottom-8">
                    <span class="material-symbols-outlined relative top-1 mr-1">
                        logout
                    </span>
                    <Link on:click={logout} href="login">
                        Log Out
                    </Link>
                </li>
            </ul>
        </nav>
    </aside>
</div>

<style>
    aside {
        transform: translateX(-120%);
        transition: transform 0.3s ease-in-out;
        border-radius: 0 40px 0 0;
    }
    
    .open {
        transform: initial;
    }

    span {
        color: white;
    }
</style>