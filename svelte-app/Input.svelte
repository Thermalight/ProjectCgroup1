<script>
    export let value, label, type;
    function typeAction(node)
    {
        node.type = type;
    }
</script>
<label >
    <p class="text-white">{label}:</p>
    <input use:typeAction bind:value>
</label>