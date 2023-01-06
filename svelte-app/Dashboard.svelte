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
    <div style="background-color:#363e4c; height: 90%;" class="w-5/6 m-auto rounded-2xl">
        <div style="background-color:#111727;"class="flex justify-center   h-16 rounded-t-2xl p-auto">
            <h1 class="text-2xl text-white m-auto">Dashboard</h1>
        </div>
        <div style="height: 100%;Width: 100%;"class="flex flex-col">
            <div style="height: 40%;Width: 100%; z-index: 1;" class="block">
                <MapMain bind:this={mapComponent} bind:notifications={notifications} bind:changedNotifications={changedNotifications}/>
            </div>
            <div style="height: 50%; Width: 100%;" class="relative overflow-auto" >
                <List {mapComponent} addNotifs={addNotifs}/>
            </div>
        </div>
    </div>
</div>