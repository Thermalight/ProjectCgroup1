<script>
    import List from './List.svelte';

    let mapComponent
    let notifications
    let changedNotifications
    let addNotifs = (content,changed) =>{
        notifications = content
        changedNotifications = changed
    }
</script>
<div class="h-full w-full">
    <div style="background-color:#363e4c; height: 90%;" class="w-5/6 m-auto rounded-2xl">
        <div style="background-color:#111727;"class="flex justify-center   h-16 rounded-t-2xl p-auto">
            <h1 class="text-2xl text-white m-auto">Notifications</h1>
        </div>
        <div style="height: 100%;Width: 100%;"class="flex flex-col">
            <div style="height: 100%; Width: 100%;" class="relative overflow-auto" >
                <List {mapComponent} addNotifs={addNotifs}/>
            </div>
        </div>
    </div>
</div>