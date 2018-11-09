const baseurl = "http://sec31098.ica.ia-hc.net:8088/"

export function getQueue() {
  return fetch(baseurl + 'api/queue/Deli', {
    method: 'GET'
  })
  .then(response => response.json());
}

export function getTicket() {
  return fetch(baseurl + 'api/queue/ticket', {
    method: 'POST',
    headers: {
      'Accept': 'application/json, text/plain, */*',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({queueType: "Deli"})
  })
  .then(response => response.json());
}
