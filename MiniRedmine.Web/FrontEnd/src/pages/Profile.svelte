<script>
  import moment from "moment";
  import {user} from '../stores/userstore';
  const lastLogin = moment($user.last_login_on).format("YYYY-MMM-DD HH:mm Z");  
</script>
<div class="container-fluid">
  <div class="row">
    <div class="col-12">
      <div
        class="card card-inverse"
        style="background-color: #333; border-color: #333;">
        <div class="card-block">
          <div class="row">
            <div class="col-md-8 col-sm-8">
              <h2 class="card-title">
                Name: {$user.firstname} {$user.lastname}
              </h2>
              <p class="card-text">
                <strong>Login:</strong>
                {$user.login}
              </p>
              <p class="card-text">
                <strong>Last login:</strong>
                {lastLogin}
              </p>
              <p class="card-text">
                <strong>Id:</strong>
                {$user.id}
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>
