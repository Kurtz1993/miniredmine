<script>
  import { issues } from "../stores/issuestore";
  import { user } from "../stores/userstore";
  async function handleSubmit(event) {
    event.preventDefault();
    let issueExists = false;
    let tempIssues = Array.from($issues);
    for (let index = 0; index < tempIssues.length; index++) {
      const element = tempIssues[index];
      if (element.id === newIssue) {
        issueExists = true;
      }
    }
    if (issueExists === false) {
      const res = await fetch(
        `api/redmine/issue/${newIssue}?userApiKey=${$user.api_key}`
      );
      tempIssues.push(await res.json());
      issues.updateIssues(tempIssues);
    }
  }

  function handleRemove(event, id) {
    event.preventDefault();
    let deleteIndex = -1;
    let tempIssues = Array.from($issues);
    for (let index = 0; index < tempIssues.length; index++) {
      const element = tempIssues[index];
      if (element.id === id) {
        deleteIndex = index;
      }
    }
    if (deleteIndex >= 0) {
      tempIssues.splice(deleteIndex, 1);
      issues.updateIssues(tempIssues);
    }
  }

  let newIssue = 0;
</script>

<div class="container">
  <div class="row">
    <div class="col">
      <form on:submit={handleSubmit}>
        <input
          type="number"
          min="1"
          max="9999999"
          class="form-control"
          bind:value={newIssue} />
        <button type="submit" class="btn btn-sm btn-success">
          <i class="fas fa-plus-circle" />
        </button>
      </form>
    </div>
    <div class="col">
      <table class="table table-striped">
        <thead>
          <tr>
            <th>Issue</th>
            <th>Subject</th>
            <th />
          </tr>
        </thead>
        <tbody>
          {#each $issues as issue (issue.id)}
            <tr>
              <td>{issue.id}</td>
              <td>{issue.subject}</td>
              <td>
                <button
                  type="button"
                  class="btn btn-sm btn-danger"
                  on:click={(e) => handleRemove(e, issue.id)}>
                  <i class="fas fa-trash" />
                </button>
              </td>
            </tr>
          {/each}
        </tbody>
      </table>
    </div>
  </div>
</div>