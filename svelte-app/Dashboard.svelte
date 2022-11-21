<script>
    import Navbar from './Navbar.svelte'
</script>
<!-- <Navbar/> -->
<div class="dashboard-content">
    <div class="map-header">
        <div class="sorted-button">
            <button type="button" class="rounded-full ..." ><span class="material-symbols-outlined"> sort </span></button>
        </div>

        <h1> map </h1>

        <div class="filter-button">
            <button type="button" class="rounded-full ..."><span class="material-symbols-outlined"> filter_alt </span></button>
        </div>
    </div>

    <div class="placeholder-map">
        <p> this is a placeholder map </p>
    </div>

    <div class="placeholder-list-of-notifications">
        <div class="scroll-bar"> 
            <p> scrolly scrolly </p>
        </div>

        <div class="placeholder-notification-1">
            <p> this is placeholder notification 1 </p>
        </div>
    </div>
</div>

<div class="footer">
    <p> this is a footer </p>
</div>