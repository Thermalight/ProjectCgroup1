<script>
    export let value, label, type, icon = undefined;
    function typeAction(node)
    {
        node.type = type;
    }
</script>

<div class="mb-4 w-full relative m-auto">
    { #if icon }
        <span class="material-symbols-outlined text-white absolute top-2 -left-6">
            { icon }
        </span>
    { /if }
    <input use:typeAction placeholder={label} bind:value>
</div>

<style>
    /* create a style for the input field with only a white bottom border */
    input {
        border: none;
        border-bottom: 1px solid white;
        background-color: #111727;
        color: white;
        padding: 0.5rem;
        width: 100%;
    }

    ::placeholder {
        color: white;
    }
</style>