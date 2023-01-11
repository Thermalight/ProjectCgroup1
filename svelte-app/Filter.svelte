<script>
    import { currentPage, probability, soundType, notification_count, notifications } from "./stores.js";

    let open = false;
    let useRange = false;
    let useProbability = false;
    let useSoundType = false;

    export let changed
    export let filterBool = false
    export let handleSubmit
    
    const returnNada = () => '';
    function getEvents() {
        fetch("https://" + window.location.host + "/notifications?"+ new URLSearchParams({
            limit: useRange ? $notification_count : 10,
            soundType: useSoundType ? $soundType : "",
            probability: useProbability ? $probability : -1
        }), {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                "Authorization": 'Bearer ' + localStorage.getItem("token")
            },
        })
        .then(response => response.json())
        .then(data => $notifications = data)
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
            $notification_count = 10
        }
        if (!useSoundType){
            $soundType = ""
        }
        if (!useProbability){
            $probability = 0
        }
    }
</script>

<div class="mb-3">
    <button style="height: 24px;" on:click={openFilter} class="border-0 focus:border-transparent focus:ring-0 active:!bg-transparent">
        <span class="material-symbols-outlined text-white bg-primary-dark p-3 rounded-full">
            settings
        </span>
    </button>
    { #if changed && filterBool }
        {returnNada(handleSubmit())}
        {returnNada(changed = false)}
    { /if }
    { #if open}
        <div class="mt-3">
            <p class="text-white">Max notifications: {$notification_count}</p>
            <input type="checkbox" bind:checked={useRange}>
            { #if $currentPage == "notificationpage" }
                <input type="number" min="1" disabled={useRange ? "" : "disabled" } bind:value={$notification_count}>
            { :else }
                <input type="range" min="1" max="10" disabled={useRange ? "" : "disabled" } bind:value={$notification_count}>
            { /if }
            <br>
            <p class="text-white">Minimum probability: {$probability ? $probability : "not set"}</p>
            <input type="checkbox" bind:checked={useProbability}>
            <input type="range" disabled={useProbability ? "" : "disabled" } min="0" max="100" bind:value={$probability}><br>
            <p class="text-white">Selected soundtype: {$soundType ? $soundType : "not set"}</p>
            <input type="checkbox" bind:checked={useSoundType}>
            <select type="range" disabled={useSoundType ? "" : "disabled" } bind:value={$soundType}>
                <option value="gunshot">Gunshot</option>
                <option value="vehicle">Vehicle</option>
                <option value="animal">Animal</option>
                <option value="thunder">Thunder</option>
                <option value="unknown">Unknown</option>
            </select>
        </div>
        <button class="text-white mt-2 bg-primary-dark p-2 border-0 focus:border-transparent focus:ring-0 rounded-md" on:click={() => {
            onSave()
        }}>Save</button>
    { /if }
</div>
