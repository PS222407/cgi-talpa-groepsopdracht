<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\App;
use Illuminate\Support\Facades\DB;
use Illuminate\Support\Facades\Schema;

return new class extends Migration {
    /**
     * Run the migrations.
     */
    public function up(): void
    {
        $tables = [
            'DateVotes',
            'OutingDates',
            'Outings',
            'OutingSuggestion',
            'Restrictions',
            'RestrictionSuggestion',
            'SuggestionDates',
            'Suggestions',
            'SuggestionVote',
            'Teams',
        ];

        DB::statement('SET FOREIGN_KEY_CHECKS=0;');
        foreach ($tables as $table) {
            DB::table($table)->truncate();
        }
        DB::statement('SET FOREIGN_KEY_CHECKS=1;');

        if (App::isProduction()) {
            $employeeId = 'auth0|65398b93a9576db066b701c5';
            $managerId = 'auth0|65398b42f469896b4a1ad196';
            $adminId = 'auth0|65398adcf469896b4a1ad150';
        } else {
            $employeeId = 'auth0|65114a310a3128c2f4492e9f';
            $managerId = 'auth0|6511c2f00a3128c2f449b037';
            $adminId = 'auth0|6511496df18c062bb839864a';
        }

        DB::table('Teams')->insert([
            'id' => 1,
            'name' => 'Team 1',
        ]);

        DB::table('Restrictions')->insert([
            [
                'id' => 1,
                'name' => 'Beperking 1',
            ],
            [
                'id' => 2,
                'name' => 'Beperking 2',
            ],
            [
                'id' => 3,
                'name' => 'Beperking 3',
            ],
        ]);

        DB::table('Suggestions')->insert([
            [
                'name' => 'Bowlen',
                'userid' => $managerId,
                'description' => 'Bowlen in de plaatselijke bowlingbaan',
                'ImageUrl' => '/uploads/images/870bc7ff-ff94-4083-adde-c8d5f0c8cd0a.jpg',
            ],
            [
                'name' => 'Boogschieten',
                'userid' => $employeeId,
                'description' => 'Lekker boogschieten',
                'ImageUrl' => null,
            ],
            [
                'name' => 'Fietsen',
                'userid' => $managerId,
                'description' => 'Fietsen door de bossen van de Veluwe',
                'ImageUrl' => '/uploads/images/340dc5ec-01f4-4219-ac41-da44397ea43b.jpg',
            ],
            [
                'name' => 'Schaatsen',
                'userid' => $managerId,
                'description' => 'Schaatsen op de ijsbaan',
                'ImageUrl' => '/uploads/images/9ed7cd9a-b00c-42bd-a564-08d98a79859e.jpg',
            ],
            [
                'name' => 'Poolen',
                'userid' => $managerId,
                'description' => 'Een gezellig potje poolen bij de The Rex Snooker en pool Club',
                'ImageUrl' => '/uploads/images/f0a2a2d0-25d1-490d-8bcf-a51ee1c2bbfe.jpg',
            ],
        ]);

        DB::table('RestrictionSuggestion')->insert([
            [
                'RestrictionsId' => 1,
                'SuggestionsId' => 1,
            ],
            [
                'RestrictionsId' => 2,
                'SuggestionsId' => 1,
            ],
            [
                'RestrictionsId' => 1,
                'SuggestionsId' => 2,
            ],
            [
                'RestrictionsId' => 2,
                'SuggestionsId' => 2,
            ],
            [
                'RestrictionsId' => 3,
                'SuggestionsId' => 2,
            ],
        ]);

        DB::table('Outings')->insert([
            [
                'name' => 'Winter uitje',
                'teamId' => 1,
                'deadline' => now()->addWeek(),
                'ConfirmedSuggestionId' => 1,
                'ConfirmedOutingDateId' => 1,
                'ImageUrl' => '/uploads/images/70db9ffc-0411-4347-bab1-f01d4ff75e8d.jpg',
            ],
            [
                'name' => 'Tussendoor uitje',
                'teamId' => 1,
                'deadline' => now()->addWeek(),
                'ConfirmedSuggestionId' => 2,
                'ConfirmedOutingDateId' => 6,
                'ImageUrl' => '/uploads/images/870bc7ff-ff94-4083-adde-c8d5f0c8cd0a.jpg',
            ],
            [
                'name' => '25-jarig bestaansuitje',
                'teamId' => 1,
                'deadline' => now()->addWeek(),
                'ConfirmedSuggestionId' => null,
                'ConfirmedOutingDateId' => null,
                'ImageUrl' => '/uploads/images/01dd833a-a714-4c9e-9693-25c75e296bde.png',
            ],
        ]);

        DB::table('OutingDates')->insert([
            [
                'OutingId' => 1,
                'Date' => now()->addDays(28),
            ],
            [
                'OutingId' => 1,
                'Date' => now()->addDays(30),
            ],
            [
                'OutingId' => 1,
                'Date' => now()->addDays(31),
            ],
            [
                'OutingId' => 1,
                'Date' => now()->addDays(32),
            ],
            [
                'OutingId' => 2,
                'Date' => now()->addDays(28),
            ],
            [
                'OutingId' => 2,
                'Date' => now()->addDays(30),
            ],
            [
                'OutingId' => 2,
                'Date' => now()->addDays(31),
            ],
            [
                'OutingId' => 2,
                'Date' => now()->addDays(32),
            ],
            [
                'OutingId' => 3,
                'Date' => now()->addDays(28),
            ],
            [
                'OutingId' => 3,
                'Date' => now()->addDays(30),
            ],
            [
                'OutingId' => 3,
                'Date' => now()->addDays(31),
            ],
            [
                'OutingId' => 3,
                'Date' => now()->addDays(32),
            ],
        ]);

        DB::table('OutingSuggestion')->insert([
            [
                'OutingId' => 1,
                'SuggestionsId' => 1,
            ],
            [
                'OutingId' => 1,
                'SuggestionsId' => 2,
            ],
            [
                'OutingId' => 1,
                'SuggestionsId' => 3,
            ],
            [
                'OutingId' => 2,
                'SuggestionsId' => 2,
            ],
            [
                'OutingId' => 2,
                'SuggestionsId' => 3,
            ],
            [
                'OutingId' => 2,
                'SuggestionsId' => 4,
            ],
            [
                'OutingId' => 3,
                'SuggestionsId' => 3,
            ],
            [
                'OutingId' => 3,
                'SuggestionsId' => 4,
            ],
            [
                'OutingId' => 3,
                'SuggestionsId' => 5,
            ],
        ]);

        DB::table('DateVotes')->insert([
            [
                'UserId' => $managerId,
                'OutingDateId' => 4,
            ],
            [
                'UserId' => $adminId,
                'OutingDateId' => 4,
            ],
            [
                'UserId' => $employeeId,
                'OutingDateId' => 4,
            ],
            [
                'UserId' => $employeeId,
                'OutingDateId' => 5,
            ],
        ]);

        DB::table('SuggestionVote')->insert([
            [
                'UserId' => $managerId,
                'SuggestionId' => 1,
                'OutingId' => 1,
            ],
            [
                'UserId' => $adminId,
                'SuggestionId' => 1,
                'OutingId' => 1,
            ],
            [
                'UserId' => $employeeId,
                'SuggestionId' => 1,
                'OutingId' => 1,
            ],
            [
                'UserId' => $employeeId,
                'SuggestionId' => 2,
                'OutingId' => 1,
            ],
        ]);
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        $tables = [
            'DateVotes',
            'OutingDates',
            'Outings',
            'OutingSuggestion',
            'Restrictions',
            'RestrictionSuggestion',
            'SuggestionDates',
            'Suggestions',
            'SuggestionVote',
            'Teams',
        ];


        DB::statement('SET FOREIGN_KEY_CHECKS=0;');
        foreach ($tables as $table) {
            DB::table($table)->truncate();
        }
        DB::statement('SET FOREIGN_KEY_CHECKS=1;');
    }
};