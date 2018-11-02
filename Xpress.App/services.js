const baseurl = "http://sec31098.ica.ia-hc.net:8088/"

export function getQueue() {
  return fetch(baseurl + 'api/queue/Deli', {
    method: 'GET'
  })
  .then(response => response.json());
}
