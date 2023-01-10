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
    <div style="background-color:#363e4c; height: 100%;" class="w-screen m-auto relative">
        <div style="background-color:#111727;"class="flex justify-center h-16 p-auto">
            <h1 class="text-2xl text-white m-auto">Notifications</h1>
        </div>
        <div style="height: 100%;Width: 100%;"class="flex flex-col">
            <div style="height: 100%; Width: 100%;" class="relative overflow-auto pb-14" >
                <List {mapComponent} addNotifs={addNotifs}/>
            </div>
        </div>
    </div>
</div>