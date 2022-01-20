
function redirectLinksFilter() {
  $("#linkFilter").submit();
}


//statics page
function showUrls(domainId) {
  var item = $('#' + domainId);
  item.toggle(1000);
}

//static page chart
function setCountryChart(dataString, labelsString) {
  while (labelsString.includes('&quot;')) {
    labelsString = labelsString.replace('&quot;', '"');
  }
  var data = JSON.parse(dataString);
  var labels = JSON.parse(labelsString);
  var backgroundColor = [];
  var borderColor = [];
  $.each(data,
    function () {
      backgroundColor.push('rgba(75, 192, 192, 0.2)');
      borderColor.push('rgba(75, 192, 192, 1)');
    });
  var ctx = document.getElementById("countryChart").getContext('2d');
  var myChart = new Chart(ctx, {
    type: 'bar',
    data: {
      labels: labels,
      datasets: [{
        label: '# of Country visit',
        data: data,
        backgroundColor: backgroundColor,
        borderColor: borderColor,
        borderWidth: 1
      }]
    },
    options: {
      scales: {
        yAxes: [{
          ticks: {
            beginAtZero: true
          }
        }]
      }
    }
  });
}
function setDailyChart(dataString, labelsString) {
  while (labelsString.includes('&quot;')) {
    labelsString = labelsString.replace('&quot;', '"');
  }
  var data = JSON.parse(dataString);
  var labels = JSON.parse(labelsString);
  var backgroundColor = [];
  var borderColor = [];
  $.each(data,
    function () {
      backgroundColor.push('rgba(255, 99, 132, 0.2)');
      borderColor.push('rgba(255, 99, 132, 1)');
    });
  var ctx = document.getElementById("dailyChart").getContext('2d');
  var myChart = new Chart(ctx, {
    type: 'bar',
    data: {
      labels: labels,
      datasets: [{
        label: '# of Day visit',
        data: data,
        backgroundColor: backgroundColor,
        borderColor: borderColor,
        borderWidth: 1
      }]
    },
    options: {
      scales: {
        yAxes: [{
          ticks: {
            beginAtZero: true
          }
        }]
      }
    }
  });
}
function setMonthlyChart(dataString, labelsString) {
  while (labelsString.includes('&quot;')) {
    labelsString = labelsString.replace('&quot;', '"');
  }
  var data = JSON.parse(dataString);
  var labels = JSON.parse(labelsString);
  var backgroundColor = [];
  var borderColor = [];
  $.each(data,
    function () {
      backgroundColor.push('rgba(255, 206, 86, 0.2)');
      borderColor.push('rgba(255, 206, 86, 1)');
    });
  var ctx = document.getElementById("monthlyChart").getContext('2d');
  var myChart = new Chart(ctx, {
    type: 'bar',
    data: {
      labels: labels,
      datasets: [{
        label: '# of Month visit',
        data: data,
        backgroundColor: backgroundColor,
        borderColor: borderColor,
        borderWidth: 1
      }]
    },
    options: {
      scales: {
        yAxes: [{
          ticks: {
            beginAtZero: true
          }
        }]
      }
    }
  });
}