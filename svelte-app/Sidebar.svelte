<script>
    import Link from "./Link.svelte"
    import jwt_decode from "jwt-decode";
    export let open;

    const close = () => open = false;
    const logout = () => localStorage.removeItem("token");
</script>

<div class="z-50">
    { #if open }
    <div class="w-screen h-screen absolute -z-10 bg-black opacity-60 top-0 left-0" on:click={close} />
    { /if }
    <aside class="absolute bg-gray-200 w-200 h-full top-0 bottom-0" class:open>
        <nav class="p-12 text-xl">
            <ul>
                <li>
                    <Link on:click={close} href="/">Dashboard</Link>
                </li>
                <li>
                    <Link on:click={close} href="notificationpage">Notifications</Link>
                </li>
                {#if jwt_decode(localStorage.getItem("token")).admin == "True"}
                    <li>
                        <Link on:click={close} href="usermanagement">User Management</Link>
                    </li>
                {/if}
                <li>
                    <Link on:click={close} href="settings">Settings</Link>
                </li>
                <li>
                    <Link on:click={logout} href="login">Log Out</Link>
                </li>
            </ul>
        </nav>
    </aside>
</div>

<style>
    aside {
        left: -100%;
        transition: left 0.3s ease-in-out;
        border-radius: 0 40px 0 0;
    }
    
    .open {
        left: 0;
    }
</style>