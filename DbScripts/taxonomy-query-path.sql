
SELECT parent.situation
FROM [touResourceDatabase].[dbo].[situation_taxonomy] AS child,
     [touResourceDatabase].[dbo].[situation_taxonomy] AS parent
WHERE child.taxonomy_left BETWEEN parent.taxonomy_left AND parent.taxonomy_right
        AND child.situation = 'Unemployed'
ORDER BY parent.taxonomy_left;
