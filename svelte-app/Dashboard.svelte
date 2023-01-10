<script>
    import List from './List.svelte';
    import MapMain from './MapMain.svelte';
    let mapComponent
    let notifications
    export let changedNotifications
    let addNotifs = (content,changed) =>{
        notifications = content
        changedNotifications = changed
    }
</script>
<div class="h-full w-full">
    <div style="background-color:#363e4c; height: 100%;" class="w-screen m-auto relative">
        <div style="background-color:#111727;"class="flex justify-center h-16 p-auto">
            <h1 class="text-2xl text-white m-auto">Dashboard</h1>
        </div>
        <div style="height: 100%;Width: 100%; z-index: 0;"class="flex flex-col">
            <div class="w-screen h-1/2 z-0">
                <MapMain bind:this={mapComponent} bind:notifications={notifications} bind:changedNotifications={changedNotifications}/>
            </div>
            <div style="height: 50%; Width: 100%;" class="overflow-auto" >
                <List {mapComponent} addNotifs={addNotifs}/>
            </div>
        </div>
    </div>
</div>