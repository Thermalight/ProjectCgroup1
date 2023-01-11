<script>
    let open = false;
    let useRange = false;
    export let notifications;
    export let changed
    export let filterBool = false
    export let handleSubmit
    export let range
    import { currentPage } from "./stores.js";
    
    const returnNada = () => '';
    function getEvents() {
        fetch("https://" + window.location.host + "/notifications?"+ new URLSearchParams({
            limit: useRange ? range : 10
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
    }
</script>

<div class="mb-3">
    <p class="text-white">Max notifications: {range}</p>
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
            <input type="checkbox" bind:checked={useRange}>
            { #if $currentPage == "notificationpage" }
                <input type="number" min="1" disabled={useRange ? "" : "disabled" } bind:value={range}>
            { :else }
                <input type="range" min="1" max="10" disabled={useRange ? "" : "disabled" } bind:value={range}>
            { /if }
        </div>
        <button class="text-white mt-2 bg-primary-dark p-2 border-0 focus:border-transparent focus:ring-0 rounded-md" on:click={() => {
            onSave()
        }}>Save</button>
    { /if }
</div>
