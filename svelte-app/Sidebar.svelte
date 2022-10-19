<script>
    import Link from "./Link.svelte"
    export let open;
</script>

<aside class="absolute bg-gray-200 w-200 h-full" class:open>
    <nav class="p-12 text-xl">
        <ul>
            <li>
                <Link href="/">Dashboard</Link>
            </li>
            <li>
                <Link href="notificationpage">Notifications</Link>
            </li>
            <li>
                <a href="#">User Management</a>
            </li>
            <li>
                <a href="settings">Settings</a>
            </li>
        </ul>
    </nav>
</aside>

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