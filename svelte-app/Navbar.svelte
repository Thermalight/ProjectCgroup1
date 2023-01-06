<script>
    import Hamburger from './Hamburger.svelte'
    import Sidebar from './Sidebar.svelte'
    let sidebar = false
    const close = () => sidebar = false;
</script>

<div>
    <div class="flex justify-between p-2 items-center text-gray-600 w-screen">
        <nav class="flex">
            <Hamburger bind:open={sidebar}/>
            <Sidebar bind:open={sidebar}/>
        </nav>
    
        <div class="logo w-7 h-7">
            <img src="images/icon.png" alt="Chengetta logo consisting of an elephant with the words Chengetta and wildlife underneath it"/>
        </div>
    </div>
</div>