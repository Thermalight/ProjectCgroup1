<h1>Settings</h1>
<p>At this page you can change the settings.</p>
<h1>Notifications</h1>
<label for="probability"> Probability</label>
<input type="range" id="probability" min="0" max="100" value="50" oninput="rangeValue.innerText = this.value">
<p id="rangeValue">50</p>
<p>Of which status category do you want notifications? Select your options:</p>
<label for="status1"> Gunshot</label>
<input type="checkbox" id="status1" name="status1" value="Gunshot">
<label for="status2"> Car</label>
<input type="checkbox" id="status2" name="status2" value="Car">
<label for="status3"> Unknown</label>
<input type="checkbox" id="status3" name="status3" value="Unknown">
<label for="e-mail"> E-mail notification</label>
<input type="checkbox" id="e-mail" name="e-mail">
<label for="push"> Push notification</label>
<input type="checkbox" id="push" name="push">
<h1>E-mail</h1>
<label for="email">E-mailadres for notification:</label>
<input type="email" id="email" name="email">
<h1>Theme</h1>
<input list="theme">
<datalist id="theme">
    <option value="Dark">
    <option value="Light">
</datalist><br><br>
<input type="submit" value="Submit">