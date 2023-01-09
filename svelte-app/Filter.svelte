<script>
    let open = false;
    let useRange = false;
    let usePriority = false;
    let useSoundType = false;
    export let notifications;
    export let changed
    export let filterBool = false
    export let handleSubmit
    export let range = 0;
    export let priority = 0;
    export let soundType = "";
    import { currentPage } from "./stores.js";
    
    const returnNada = () => '';
    function getEvents() {
        fetch("https://" + window.location.host + "/notifications?"+ new URLSearchParams({
            limit: useRange ? range : 10,
            soundType: useSoundType ? soundType : "",
            priority: usePriority ? priority : -1
        }), {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                "Authorization": 'Bearer ' + localStorage.getItem("token")
            },
        })
        .then(response => response.json())
        .then(data => notifications = data)
        .then(() => changed = true)
    }

    function openFilter(){
        open = !open;
    }
    
    function onSave(){
        getEvents()
        filterBool = true
    }

    $: {
        if (!useRange){
            range = 10
        }
        if (!useSoundType){
            soundType = ""
        }
        if (!usePriority){
            priority = 0
        }
    }
</script>

<div>
    
    <button style="height: 24px;" class="bg-white border-0 border-transparent focus:border-transparent focus:ring-0" on:click={openFilter}><span class="material-symbols-outlined">settings</span></button>
    { #if changed && filterBool }
        {returnNada(handleSubmit())}
        {returnNada(changed = false)}
    { /if }
    { #if open}
        <div>
            <p class="text-white">Max notifications: {range}</p>
            <input type="checkbox" bind:checked={useRange}>
            { #if $currentPage == "notificationpage" }
                <input type="number" min="1" disabled={useRange ? "" : "disabled" } bind:value={range}>
            { :else }
                <input type="range" min="1" max="10" disabled={useRange ? "" : "disabled" } bind:value={range}>
            { /if }
            <br>
            <p class="text-white">Minimum priority: {priority ? priority : "not set"}</p>
            <input type="checkbox" bind:checked={usePriority}>
            <input type="range" disabled={usePriority ? "" : "disabled" } min="0" max="100" bind:value={priority}><br>
            <p class="text-white">Selected soundtype: {soundType ? soundType : "not set"}</p>
            <input type="checkbox" bind:checked={useSoundType}>
            <select type="range" disabled={useSoundType ? "" : "disabled" } bind:value={soundType}>
                <option value="gunshot">Gunshot</option>
                <option value="vehicle">Vehicle</option>
                <option value="animal">Animal</option>
                <option value="thunder">Thunder</option>
                <option value="unknown">Unknown</option>
            </select>
        </div>
        <button class="bg-white" on:click={() => {
            onSave()
        }}>Save</button>
    { /if }
</div>
