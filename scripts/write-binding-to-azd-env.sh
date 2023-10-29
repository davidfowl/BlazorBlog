#!/bin/bash
set -euo pipefail
IFS=$'\n\t'

DEBUG_CONTAINER_ENDPOINT="https://${BINDING_DEBUG_CONTAINER_FQDN}/"
azd env set SERVICE_BINDING_DB_CONNECTION_STRING "$(curl -s -H "Authorization: Bearer ${BINDING_DEBUG_CONTAINER_SHARED_KEY}" ${DEBUG_CONTAINER_ENDPOINT} | jq -r .POSTGRES_CONNECTION_STRING | sed -e 's/host=/Host=/g' -e 's/database=/Database=/g' -e 's/user=/Username=/g' -e 's/password=/Password=/g' -e 's/ /;/g')"