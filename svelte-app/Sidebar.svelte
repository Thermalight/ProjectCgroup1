<script>
    export let open;
</script>

<aside class="absolute bg-gray-200 w-200 h-full" class:open>
    <nav class="p-12 text-xl">
        <ul>
            <li>
                <a href="#">Dashboard</a>
            </li>
            <li>
                <a href="#">Notifications</a>
            </li>
            <li>
                <a href="#">User Management</a>
            </li>
            <li>
                <a href="#">Settings</a>
            </li>
        
</ul>
    </nav>
</aside>

<style>
    aside {
        left: -100%;
        transition: left 0.3s ease-in-out
    }
    
    .open {
        left: 0
    }
</style>