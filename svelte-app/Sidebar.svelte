<script>
    import Link from "./Link.svelte"
    export let open;

    const close = () => open = false;
</script>

<div>
    { #if open }
    <div class="w-screen h-screen absolute -z-10 bg-black opacity-60 top-0 left-0" on:click={close} />
    { /if }
    <aside class="absolute bg-gray-200 w-200 h-full" class:open>
        <nav class="p-12 text-xl">
            <ul>
                <li>
                    <Link on:click={close} href="/">Dashboard</Link>
                </li>
                <li>
                    <Link on:click={close} href="notificationpage">Notifications</Link>
                </li>
                <li>
                    <a on:click={close} href="#">User Management</a>
                </li>
                <li>
                    <a on:click={close} href="#">Settings</a>
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