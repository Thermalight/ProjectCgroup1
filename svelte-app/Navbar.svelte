<script>
    import Hamburger from './Hamburger.svelte'
    import Sidebar from './Sidebar.svelte'

    let sidebar = false
</script>

<div>
    <div>
        <nav class="flex">
            <div class="absolute top-4 left-4 z-40">
                <Hamburger bind:open={sidebar}/>
            </div>
            <Sidebar bind:open={sidebar}/>
        </nav>
    
        <div class="logo w-7 h-7 absolute z-40 right-4 top-4">
            <img src="images/icon.png" alt="Chengetta logo consisting of an elephant with the words Chengetta and wildlife underneath it"/>
        </div>
    </div>
</div>