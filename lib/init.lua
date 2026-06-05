-- dotnet-resource-kafka-library main module.
-- Renders Resources/Messaging.cs with Confluent.Kafka IProducer into the service project directory.
--
-- The calling archetype is responsible for adding the corresponding
-- NuGet package to the .csproj:
--   Confluent.Kafka
--
-- API:
--   local kafka = require("dotnet-resource-kafka")
--   kafka.render(context, { destination = context:get("project-name") })
--
-- Context contract:
--   prefix-name  — kebab-case first segment (e.g. "billing")
--   suffix-name  — kebab-case second segment (e.g. "service")
--   PrefixName   — PascalCase first segment (e.g. "Billing")  [set by Cases.programming()]
--   SuffixName   — PascalCase second segment (e.g. "Service") [set by Cases.programming()]

local M = {}

function M.render(context, opts)
    opts = opts or {}
    local d = opts.destination
    if d and d ~= "" then
        directory.render("contents", context, { destination = d })
    else
        directory.render("contents", context)
    end
    return context
end

return M
